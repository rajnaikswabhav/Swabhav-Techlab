import {Component} from "@angular/core";

@Component({
    selector : 'ht-welcome',
    templateUrl : 'welcomeComponent.html'
})
export class welcomeComponent {
    message:string = "Welcome to Angular 4";

    public get Message(){
        return this.message;
    }
}