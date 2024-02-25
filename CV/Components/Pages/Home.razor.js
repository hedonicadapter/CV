document.addEventListener('DOMContentLoaded', () => {
  setTimeout(() => document.body.lastChild.remove(), 1000);
});

function setProfileBody(body) {
  const profile = document.querySelector('#profil-text');
  if (!profile || !body) return;

  const parser = new DOMParser();
  const doc = parser.parseFromString(body, 'text/html');
  profile.innerHTML = `${doc.body.innerHTML}`;
}
function setProfileBodyPlainText(body) {
  const profile = document.querySelector('#profil-text');
  if (!profile || !body) return;
  profile.textContent = body.toString();
}
function getProfileText() {
  const profile = document.querySelector('#profil-text');
  return profile.textContent.toString() || '';
}

function disableEveryFormControl() {
  const formControls = document.querySelectorAll(
    'input, textarea, div[contenteditable="true"]'
  );

  formControls?.forEach((element) => {
    element.disabled = true;
    if (element.contentEditable) element.contentEditable = false;
  });
}

function enableEveryFormControl() {
  const formControls = document.querySelectorAll(
    'input, textarea, div[contenteditable="true"]'
  );

  formControls?.forEach((element) => {
    element.disabled = false;
    if (element.contentEditable) element.contentEditable = true;
  });
}

function getComputedStyles(element) {
  var styleTag = document.createElement('style');
  var cssText = '';

  function processElem(elem) {
    var css = window.getComputedStyle(elem);
    var rule = elem.tagName.toLowerCase() + '#' + (elem.id || '') + '{';

    for (var prop of css) {
      if (css[prop]) {
        rule += prop + ':' + css[prop] + ';';
      }
    }

    rule += '}';
    cssText += rule;

    for (var child of elem.children) {
      processElem(child);
    }
  }

  processElem(element);
  styleTag.textContent = cssText;

  return styleTag;
}

function htmlBoilerplate() {
  let doctype = document.implementation.createDocumentType('html', '', '');
  document.doctype
    ? document.replaceChild(doctype, document.doctype)
    : document.appendChild(doctype);

  let html = document.documentElement;
  html.lang = 'en';

  // Create <head>
  let head = document.head || html.appendChild(document.createElement('head'));

  let metaCharset = document.createElement('meta');
  metaCharset.setAttribute('charset', 'UTF-8');
  head.appendChild(metaCharset);

  let metaViewport = document.createElement('meta');
  metaViewport.setAttribute('name', 'viewport');
  metaViewport.setAttribute('content', 'width=device-width, initial-scale=1.0');
  head.appendChild(metaViewport);

  let title = document.createElement('title');
  title.textContent = 'Document';
  head.appendChild(title);

  if (!document.body) html.appendChild(document.createElement('body'));

  return html;
}

function getCV() {
  const clone = document.cloneNode(true);
  const CV = clone.querySelector('#cv');

  const adminContents = CV?.querySelectorAll('.admin-content');
  adminContents?.forEach((element) => {
    element.remove();
  });

  clone.body.innerHTML = CV.outerHTML;

  return clone.documentElement.outerHTML;
}
