function printDetail(customers) {
    for (var _i = 0, customers_1 = customers; _i < customers_1.length; _i++) {
        var c = customers_1[_i];
        console.log(c);
    }
}
printDetail([{ customerName: 'Akash', lastName: 'Malaviya', moneySpent: 1000 },
{ customerName: 'Brijesh', lastName: '', moneySpent: 500 },
{ customerName: 'Parth', lastName: 'Jodhani', moneySpent: 2000 }
]);
