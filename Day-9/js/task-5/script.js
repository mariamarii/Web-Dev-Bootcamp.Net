
const text2 = "100020003000"

const unique = new Set(text2);
unique.delete('0');
console.log([...unique].join(''));