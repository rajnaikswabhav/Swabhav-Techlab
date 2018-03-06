import { Component } from '@angular/core';

import { HomePage } from '../home/home';
import { EditPage } from './../EditNote/EditNote';
import { AddPage } from './../AddNote/AddNote';

@Component({
  templateUrl: 'tabs.html'
})
export class TabsPage {

  tab1Root = HomePage;
  tab2Root = AddPage;

  constructor() {

  }
}
