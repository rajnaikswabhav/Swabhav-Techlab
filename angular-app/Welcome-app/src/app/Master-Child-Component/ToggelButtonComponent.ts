import { OnInit, Component, Input } from "@angular/core";
import { EventEmitter } from "@angular/core";
import { Output } from "@angular/core";


@Component({
    selector: 'ht-toggel-btn',
    templateUrl: 'ToggelButtonComponent.html'
})
export class ToggeLButtonComponent implements OnInit {

    @Input()
    textContent: string;
    @Input()
    onColor: string;
    @Input()
    offColor: string;
    bgValidationColor: string;
    @Output()
    stateChange: EventEmitter<string> = new EventEmitter<string>();
    flag: number;

    ngOnInit(): void {
        console.log("Inside InIt...");
        this.flag = 1;
        //this.textContent = "Nothing Again";
    }

    constructor() {
        this.textContent = "Default";
        this.onColor = "green";
        this.offColor = "red";
        console.log("Inside Constructor");
    }

    onClickHandler() {
        // console.log("You clicked Me...");
        if (this.flag == 1) {
            this.stateChange.emit("ON State.....");
            this.flag = 0;
            this.bgValidationColor = this.onColor;
        }
        else if (this.flag == 0) {
            this.stateChange.emit("OFF State.....");
            this.flag = 1;
            this.bgValidationColor = this.offColor;
        }
    }
}