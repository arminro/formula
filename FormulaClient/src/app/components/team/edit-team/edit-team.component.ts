import { PigeonService } from '../../../services/pigeon.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TeamsService } from 'src/app/services/teams.service';
import { AuthService } from 'src/app/services/auth.service';
import { TeamComponentBase } from '../teamcomponent-base';
import { FormulaTeam } from 'src/app/viewmodels/formulateam-model';
import { AppConfig } from 'src/appconfig';

@Component({
  selector: 'app-edit-team',
  templateUrl: './edit-team.component.html',
  styleUrls: ['./edit-team.component.css']
})
export class EditTeamComponent extends TeamComponentBase implements OnInit {

  constructor(public router: Router, public teamSrv: TeamsService, public authSrv: AuthService,
              private readonly pigeonSrv: PigeonService) {
      super(router, teamSrv, authSrv);
   }

  currentTeam: FormulaTeam;
  ngOnInit() {
    this.pigeonSrv.getCurrentData
    .subscribe(jsonData => {
        this.currentTeam = JSON.parse(jsonData);
    });
  }

  onSubmit() {
    this.teamSrv.updateElement(this.currentTeam)
    .subscribe(
        data => {
            this.redirectTo('me');
          });
  }

  onCancel() {
    this.redirectTo('me');
  }

}
