document.querySelector('form').addEventListener('submit', (e) => {
    e.preventDefault();
  
    const numberOfElements = document.querySelector('input[name="elements"]').value;
    const elementText = document.querySelector('input[name="texts"]').value;
    const elementType = document.querySelector('select[name="type"]').value;
    const resultsContainer = document.querySelector('.results');
  
    resultsContainer.innerHTML = '';
  
    if (numberOfElements && elementText) {
      for (let i = 0; i < numberOfElements; i++) {
        const newElement = document.createElement(elementType);
        newElement.textContent = `${elementText} ${i + 1}`;
        resultsContainer.appendChild(newElement);
      }
    } 
  });