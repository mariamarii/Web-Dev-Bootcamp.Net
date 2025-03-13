let myString = "EElllzzzzzzzeroo";

str=[...myString].filter((x,index)=> x!==myString[index-1]).join("");
console.log(str)