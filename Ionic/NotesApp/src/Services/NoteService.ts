import { Injectable } from "@angular/core";
import { Storage } from "@ionic/storage";

@Injectable()
export class NoteService {

    id: number;

    listOfNotes = []

    constructor(private storage: Storage) {
        if (this.listOfNotes.length != null) {
            this.storage.get('notes')
                .then(m => {
                    let note = JSON.parse(m);
                    for (let n of note) {
                        this.listOfNotes.push(n);
                    }
                })
                .catch(e => console.log("Error is: ", e));
        }
    }

    Add(note) {
        this.listOfNotes.push(note);
        this.storage.set('notes', JSON.stringify(this.listOfNotes));
    }

    GetDataById(id) {
        console.log(id);

        for (let data of this.listOfNotes) {
            if (data.id == id) {
                return data;
            }
        }
    }

    EditNote(id, title, desc) {
        console.log(id, title, desc);

        for (let d = 0; d < this.listOfNotes.length; d++) {
            if (this.listOfNotes[d].id == id) {
                this.listOfNotes[d].title = title;
                this.listOfNotes[d].desc = desc;
            }
        }

        this.storage.set('notes', JSON.stringify(this.listOfNotes));
    }

    Remove(id) {
        for (let d = 0; d < this.listOfNotes.length; d++) {
            if (this.listOfNotes[d].id == id) {
                this.listOfNotes.splice(d, 1);
            }
        }

        this.storage.set('notes', JSON.stringify(this.listOfNotes));
    }

    Reorder(indexes) {
        console.log(indexes);

        let element = this.listOfNotes[indexes.from];
        this.listOfNotes.splice(indexes.from, 1);
        this.listOfNotes.splice(indexes.to, 0, element);
    }

    SaveReorder(){
        this.storage.set('notes', JSON.stringify(this.listOfNotes));
    }
}