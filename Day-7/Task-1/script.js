let mix = [1, 2, 3, "E", 4, "l", "z", "e", "r", 5, "o"];

const str = mix.map(x => typeof x === "number" ? null:x).filter(x => x !== null );

console.log(str);

 conc_str=str.reduce((acc,x) => acc+x,"")
console.log(conc_str);