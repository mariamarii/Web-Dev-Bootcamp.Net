let chars = ["A", "B", "C", 20, "D", "E", 10, 15, 6];
let letters = chars.filter(char => typeof char === "string");
let numberCount = chars.filter(item => typeof item === "number").length;

let replaced = letters.slice(0,numberCount)
result=[...replaced,...letters]
console.log(result); 
