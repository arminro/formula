import { AppConfig } from './../../../../appconfig';
import { TeamComponentBase } from './../teamcomponent-base';

import { FormulaTeam } from '../../../viewmodels/formulateam-model';
import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { TeamsService } from 'src/app/services/teams.service';


@Component({
  selector: 'app-add-job',
  templateUrl: './add-team.component.html',
  styleUrls: ['./add-team.component.css']
})
export class AddTeamComponent extends TeamComponentBase implements OnInit {

  constructor(public router: Router, public teamSrv: TeamsService, public authSrv: AuthService) {
    super(router, teamSrv, authSrv);
  }

  teamToAdd: FormulaTeam;
  ngOnInit(): void {
    this.teamToAdd = new FormulaTeam();
  }

  onSubmit() {
    this.teamSrv.createElement(this.teamToAdd)
    .subscribe(
        data => {
            this.redirectTo('me');
          });
  }

  onCancel() {
    this.redirectTo('me');
  }

}
