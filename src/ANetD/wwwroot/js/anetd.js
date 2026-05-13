// ANetD JS Interop Module
// Loaded lazily on first use via ANetDJSInterop service.
// All exports called from C# via IJSObjectReference.

// ─── Focus Management ─────────────────────────────────────────────────────

export function focusElement(element) {
  if (element) {
    element.focus({ preventScroll: false });
  }
}

export function blurElement(element) {
  if (element) element.blur();
}

// ─── Focus Trap ───────────────────────────────────────────────────────────

const focusTraps = new Map();

const FOCUSABLE_SELECTORS = [
  'a[href]',
  'button:not([disabled])',
  'input:not([disabled])',
  'select:not([disabled])',
  'textarea:not([disabled])',
  '[tabindex]:not([tabindex="-1"])',
  'details > summary',
].join(',');

export function trapFocus(containerId, element) {
  if (!element) return;

  const getFocusable = () => Array.from(element.querySelectorAll(FOCUSABLE_SELECTORS));

  const handler = (e) => {
    if (e.key !== 'Tab') return;
    const focusable = getFocusable();
    if (!focusable.length) { e.preventDefault(); return; }

    const first = focusable[0];
    const last = focusable[focusable.length - 1];

    if (e.shiftKey) {
      if (document.activeElement === first) { e.preventDefault(); last.focus(); }
    } else {
      if (document.activeElement === last) { e.preventDefault(); first.focus(); }
    }
  };

  document.addEventListener('keydown', handler);
  focusTraps.set(containerId, handler);

  // Focus first element
  const firstFocusable = getFocusable()[0];
  if (firstFocusable) firstFocusable.focus();
}

export function releaseFocusTrap(containerId) {
  const handler = focusTraps.get(containerId);
  if (handler) {
    document.removeEventListener('keydown', handler);
    focusTraps.delete(containerId);
  }
}

// ─── Click Outside Detection ──────────────────────────────────────────────

const clickOutsideHandlers = new Map();

export function addClickOutsideListener(listenerId, element, dotnetRef, callbackMethod) {
  if (!element) return;

  const handler = (e) => {
    if (!element.contains(e.target)) {
      dotnetRef.invokeMethodAsync(callbackMethod);
    }
  };

  document.addEventListener('mousedown', handler, true);
  clickOutsideHandlers.set(listenerId, handler);
}

export function removeClickOutsideListener(listenerId) {
  const handler = clickOutsideHandlers.get(listenerId);
  if (handler) {
    document.removeEventListener('mousedown', handler, true);
    clickOutsideHandlers.delete(listenerId);
  }
}

// ─── Scroll Lock ──────────────────────────────────────────────────────────

let scrollLockCount = 0;
let savedScrollbarWidth = 0;

export function lockBodyScroll() {
  if (scrollLockCount === 0) {
    const scrollbarWidth = window.innerWidth - document.documentElement.clientWidth;
    savedScrollbarWidth = scrollbarWidth;
    document.body.style.overflow = 'hidden';
    if (scrollbarWidth > 0) {
      document.body.style.paddingRight = `${scrollbarWidth}px`;
    }
  }
  scrollLockCount++;
}

export function unlockBodyScroll() {
  scrollLockCount = Math.max(0, scrollLockCount - 1);
  if (scrollLockCount === 0) {
    document.body.style.overflow = '';
    document.body.style.paddingRight = '';
  }
}

// ─── Clipboard ────────────────────────────────────────────────────────────

export async function copyToClipboard(text) {
  if (!navigator.clipboard) {
    // Fallback
    const el = document.createElement('textarea');
    el.value = text;
    el.style.position = 'fixed';
    el.style.opacity = '0';
    document.body.appendChild(el);
    el.select();
    document.execCommand('copy');
    document.body.removeChild(el);
    return true;
  }
  try {
    await navigator.clipboard.writeText(text);
    return true;
  } catch {
    return false;
  }
}

// ─── Scroll Metrics ───────────────────────────────────────────────────────

export function getScrollMetrics(element) {
  if (!element) return { scrollTop: 0, scrollHeight: 0, clientHeight: 0 };
  return {
    scrollTop: element.scrollTop,
    scrollHeight: element.scrollHeight,
    clientHeight: element.clientHeight,
  };
}

export function scrollToElement(element, behavior) {
  if (element) element.scrollIntoView({ behavior: behavior || 'smooth', block: 'nearest' });
}

// ─── Resize Observer ──────────────────────────────────────────────────────

const resizeObservers = new Map();

export function observeResize(observerId, element, dotnetRef, callbackMethod) {
  if (!element) return;

  const observer = new ResizeObserver(entries => {
    for (const entry of entries) {
      const { width, height } = entry.contentRect;
      dotnetRef.invokeMethodAsync(callbackMethod, Math.round(width), Math.round(height));
    }
  });

  observer.observe(element);
  resizeObservers.set(observerId, observer);
}

export function unobserveResize(observerId) {
  const observer = resizeObservers.get(observerId);
  if (observer) {
    observer.disconnect();
    resizeObservers.delete(observerId);
  }
}

// ─── Element Positioning (Floating UI-like logic) ─────────────────────────

export function getElementRect(element) {
  if (!element) return null;
  const rect = element.getBoundingClientRect();
  return {
    top: rect.top,
    left: rect.left,
    bottom: rect.bottom,
    right: rect.right,
    width: rect.width,
    height: rect.height,
  };
}

export function positionFloating(floatingEl, referenceEl, placement, offset) {
  if (!floatingEl || !referenceEl) return;

  const refRect = referenceEl.getBoundingClientRect();
  const floatRect = floatingEl.getBoundingClientRect();
  const gap = offset || 4;
  const vw = window.innerWidth;
  const vh = window.innerHeight;

  let top = 0;
  let left = 0;

  const placements = {
    top:          () => ({ top: refRect.top - floatRect.height - gap, left: refRect.left + refRect.width / 2 - floatRect.width / 2 }),
    'top-start':  () => ({ top: refRect.top - floatRect.height - gap, left: refRect.left }),
    'top-end':    () => ({ top: refRect.top - floatRect.height - gap, left: refRect.right - floatRect.width }),
    bottom:       () => ({ top: refRect.bottom + gap, left: refRect.left + refRect.width / 2 - floatRect.width / 2 }),
    'bottom-start': () => ({ top: refRect.bottom + gap, left: refRect.left }),
    'bottom-end': () => ({ top: refRect.bottom + gap, left: refRect.right - floatRect.width }),
    left:         () => ({ top: refRect.top + refRect.height / 2 - floatRect.height / 2, left: refRect.left - floatRect.width - gap }),
    right:        () => ({ top: refRect.top + refRect.height / 2 - floatRect.height / 2, left: refRect.right + gap }),
  };

  const compute = placements[placement] || placements['bottom'];
  const pos = compute();
  top = pos.top;
  left = pos.left;

  // Clamp to viewport
  top = Math.max(8, Math.min(top, vh - floatRect.height - 8));
  left = Math.max(8, Math.min(left, vw - floatRect.width - 8));

  floatingEl.style.position = 'fixed';
  floatingEl.style.top = `${top}px`;
  floatingEl.style.left = `${left}px`;
}

// ─── Theme ────────────────────────────────────────────────────────────────

export function setTheme(theme, element) {
  const target = element || document.documentElement;
  if (theme) {
    target.setAttribute('data-theme', theme);
  } else {
    target.removeAttribute('data-theme');
  }
}

export function getTheme(element) {
  const target = element || document.documentElement;
  return target.getAttribute('data-theme') || 'light';
}

export function getSystemThemePreference() {
  return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
}

// ─── Portal / Teleport ────────────────────────────────────────────────────

export function teleportToBody(element) {
  if (element && element.parentNode !== document.body) {
    document.body.appendChild(element);
  }
}
