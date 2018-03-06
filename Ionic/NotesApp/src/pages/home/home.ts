import { NoteService } from './../../Services/NoteService';
import { EditPage } from './../EditNote/EditNote';
import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { AlertController } from 'ionic-angular/components/alert/alert-controller';


@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {

  notesList = [];
  reorderValue = false;
  constructor(private navCtrl: NavController, private noteService: NoteService,
    private alertCtrl: AlertController) { }

  ngOnInit() {
    this.notesList = this.noteService.listOfNotes;
  }

  EditNote(id) {
    console.log(id);
    this.noteService.id = id;
    this.navCtrl.push(EditPage, { Id: id });
  }

  RemoveNote(id) {
    let alert = this.alertCtrl.create({
      title: 'Delete Item',
      message: 'Are you sure, you want to delete this item?',
      buttons: [
        {
          text: 'DISAGREE',
          role: 'cancel',
          handler: () => {
            console.log("Cancel clicked");
          }
        },
        {
          text: 'AGREE',
          handler: () => {
            this.noteService.Remove(id);
          }
        }
      ]
    });
    alert.present();
  }

  ReorderItems(indexes) {
    this.noteService.Reorder(indexes);
  }

  ReorderNotes() {
    if (this.reorderValue == false) {
      this.reorderValue = true;
    }
    else {
      this.reorderValue = false;
      this.noteService.SaveReorder();
    }

  }
}
