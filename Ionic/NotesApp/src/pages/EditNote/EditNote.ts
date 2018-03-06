import { Component } from '@angular/core';
import { NoteService } from './../../Services/NoteService';
import { NavController } from 'ionic-angular';
import { NavParams } from 'ionic-angular/navigation/nav-params';

@Component({
  selector : 'note-edit',
  templateUrl : 'EditNote.html'
})
export class EditPage {

  id:number;
  title:string;
  desc:string;

  constructor (private navCtrl : NavController , private  noteService : NoteService,
  private navParams : NavParams){
  }

  ngOnInit(){
    this.id = this.navParams.get('Id');
    let editNote = this.noteService.GetDataById(this.id);
    this.title = editNote.title;
    this.desc = editNote.desc;
  }

  EditData(title,desc){
    this.noteService.EditNote(this.id,title,desc);
    this.navCtrl.pop();
  }
}

