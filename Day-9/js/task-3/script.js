let myInfo = {
    username: "Osama",
    role: "Admin",
    country: "Egypt",
  };
  
const map1=new Map(Object.entries(myInfo));
console.log(map1);
console.log(map1.size);
console.log(map1.has("role"));

