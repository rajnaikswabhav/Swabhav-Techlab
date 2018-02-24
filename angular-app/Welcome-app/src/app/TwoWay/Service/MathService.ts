


export class MathService {
    CheckIsNumPrime(numToCheck) {
        let p = new Promise<number>((resolve, reject) => {
            let m = numToCheck / 2;
            let flag = 0;
            if (numToCheck == 0 || numToCheck == 1) {
                resolve(0);
            } else {
                for (let i = 2; i <= m; i++) {
                    if (numToCheck % i == 0) {
                        flag = 1;
                        resolve(0);
                        break;
                    }
                }
                if (flag == 0) {
                    resolve(1);
                }
            }
        });
        return p;
    }
}