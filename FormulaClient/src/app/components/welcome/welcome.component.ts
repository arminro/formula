import { PigeonService } from '../../services/pigeon.service';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/viewmodels/user-model';
import { FormulaTeam } from 'src/app/viewmodels/formulateam-model';
import { TeamsService } from 'src/app/services/teams.service';
import { ComponentBase } from '../component-base';


@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.css']
})
export class WelcomeComponent extends ComponentBase implements OnInit {
  user: User;
  pageTitle: string;
  teams: FormulaTeam[];

  constructor(
    private readonly teamSrv: TeamsService,
    public authSrv: AuthService,
    public router: Router,
    private readonly pigeonSrv: PigeonService) {
        super(router);
     }

  ngOnInit() {

    this.pageTitle = `Greetings, ${this.authSrv.currentUserValue !== null ? this.authSrv.currentUserValue.name : 'anonymous'}!`;
    this.teamSrv.getElements()
    .subscribe(
        (data: FormulaTeam[]) => {
          if (data) {
            this.teams = data;
          }});
  }

  addTeamClick() {
    this.redirectTo('teams/add');
  }

  editTeamClick(currentTeam: FormulaTeam) {
    this.pigeonSrv.sendData(JSON.stringify(currentTeam));
    this.redirectTo('teams/edit');
  }
  deleteTeamClick(currentTeam: FormulaTeam) {
    this.pigeonSrv.sendData(JSON.stringify(currentTeam));
    this.redirectTo('teams/delete');
  }
}
