import { Component } from "@angular/core";


@Component({
    selector: 'ht-blog',
    templateUrl: 'BlogComponent.html'
})
export class BlogComponent {
    message: string;
    messages = [];

    AddMessage(message) {
        if (this.message == null) {
            this.messages.push(message.target.value);
        }
        this.StoreMessages();
        message.target.value = "";
    }

    StoreMessages() {
        localStorage.setItem("messages", JSON.stringify(this.messages));
        console.log(this.messages);
    }

    IntializeList() {
        let previousMessage = JSON.parse(localStorage.getItem("messages"));
        if (previousMessage) {
            for (let msg of previousMessage) {
                this.messages.push(msg);
            }
        }
    }

    ngOnInit() {
        this.IntializeList();
        console.log("Inside OnLoad..");
    }

    ClearItem($event) {
        let id = $event.target.id;
        console.log("Inside ClearItem");
        console.log(id);
        for(let i=0; i<this.messages.length; i++){
            if(this.messages[i] == id){
                this.messages.splice(i,1);
                localStorage.setItem("messages",JSON.stringify(this.messages));
            }
        }

    }
}