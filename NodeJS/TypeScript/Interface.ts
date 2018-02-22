interface ICustomer {
    customerName: string,
    lastName?: string,
    moneySpent: number
}

function printDetail(customers: ICustomer[]) {
    for (let c of customers) {
        console.log(c);
    }
}

printDetail([{ customerName: 'Akash', lastName: 'Malaviya', moneySpent: 1000 },
{ customerName: 'Brijesh', lastName: '', moneySpent: 500 },
{ customerName: 'Parth', lastName: 'Jodhani', moneySpent: 2000 }
]);