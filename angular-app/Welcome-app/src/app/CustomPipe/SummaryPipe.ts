import { PipeTransform } from "@angular/core";
import { Pipe } from "@angular/core";


@Pipe({
    name : 'summary'
})
export class SummaryPipe implements PipeTransform{

    transform(value: string,num:number) {
        if(num == 0){
            return value;
        }
        else if(num >= value.length){
            return alert("Givet string length is short then number...");
        }
        else{
                return value.substr(0,num);
        }
    }

}