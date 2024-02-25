function autoSize(element) {
  switch (element.tagName) {
    case 'TEXTAREA':
    case 'INPUT':
      if (element.type === 'date' || element.type === 'month') {
        return autoSizeDateInput(element);
      } else {
        return autoSizeTextarea(element);
      }
  }
}

function autoSizeTextarea(textarea) {
  const referenceElement =
    textarea.parentElement.querySelector('.size-reference');
  referenceElement.innerText = textarea.value;

  const { lineHeight } = window.getComputedStyle(textarea);
  const lines = referenceElement.scrollHeight / parseInt(lineHeight, 10);
  const roundedUp = Math.round(lines);

  roundedUp > 0
    ? textarea.setAttribute('rows', roundedUp)
    : textarea.setAttribute('rows', 1);
}

function autoSizeTextareas() {
  setTimeout(() => {
    const textAreas = document.querySelectorAll('textarea');
    textAreas.forEach(autoSizeTextarea);
  }, 100);
}

function autoSizeDateInput(dateInput) {
  const referenceElement =
    dateInput.parentElement.querySelector('.size-reference');
  dateInput.style.width = referenceElement?.offsetWidth + 'px';
}

function autoSizeDateInputs() {
  const dateInputs = document.querySelectorAll(
    'input[type="date"], input[type="month"]'
  );
  dateInputs?.forEach(autoSizeDateInput);
}

function scrollToElement(elementId) {
  setTimeout(() => {
    document.getElementById(elementId)?.scrollIntoView({
      behavior: 'smooth',
      block: 'center',
    });
  }, 100);
}

function resetTagsInput(containerId) {
  const tagsInput = document
    .getElementById(containerId)
    ?.querySelector('.tags-input');
  if (!tagsInput) return;
  tagsInput.value = '';
}

function init() {
  autoSizeTextareas();
  autoSizeDateInputs();
}

//TODO: also run after clicking "Add"
document.addEventListener('DOMContentLoaded', init);
window.addEventListener('resize', init);
