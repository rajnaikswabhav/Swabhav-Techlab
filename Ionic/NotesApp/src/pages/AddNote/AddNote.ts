import { UUID } from 'angular2-uuid';
import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { NoteService } from '../../Services/NoteService';

@Component({
  selector: 'note-add',
  templateUrl: 'AddNote.html'
})
export class AddPage {

  id: number;
  title = "";
  desc = "";
  constructor(public navCtrl: NavController, private noteSer: NoteService) {

  }

  AddNote() {
    if (this.title == "" || this.desc == "") {
      alert("Something missing..");
    }
    else {
      let newNote = {
        id:UUID.UUID(),
        title: this.title,
        desc: this.desc
      }
      console.log(newNote);
      this.noteSer.Add(newNote);
      this.navCtrl.parent.select(0);
      this.title = "";
      this.desc = "";
    }
  }

}
