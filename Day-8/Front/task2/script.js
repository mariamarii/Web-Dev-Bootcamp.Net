const imgs= document.querySelectorAll('img')
imgs.forEach((img)=>{
  if(img.hasAttribute('alt')){
    img.setAttribute('alt','Elzero New')

  }
  else{
    img.setAttribute('alt','Old')
  }
})