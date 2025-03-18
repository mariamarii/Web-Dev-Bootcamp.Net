
document.addEventListener('input', () => {
  dollarInput = document.querySelector('input');
  const resultDiv = document.querySelector('.result');
  const conversionRate = 50.36 ; 
  let dollarValue = parseFloat(dollarInput.value);

    if(isNaN(dollarValue)){
      dollarValue=0
    }
    if (!isNaN(dollarValue) && dollarValue >= 0) {
      const egpValue = (dollarValue * conversionRate).toFixed(2);
      resultDiv.textContent = `{${dollarValue}} USD Dollar = {${egpValue}} Egyptian Pound`;
    }
 
});
