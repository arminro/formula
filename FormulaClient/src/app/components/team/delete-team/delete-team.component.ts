import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TeamsService } from 'src/app/services/teams.service';
import { AuthService } from 'src/app/services/auth.service';
import { TeamComponentBase } from '../teamcomponent-base';
import { FormulaTeam } from 'src/app/viewmodels/formulateam-model';
import { PigeonService } from 'src/app/services/pigeon.service';
import { AppConfig } from 'src/appconfig';

@Component({
  selector: 'app-delete-team',
  templateUrl: './delete-team.component.html',
  styleUrls: ['./delete-team.component.css']
})
export class DeleteTeamComponent extends TeamComponentBase implements OnInit {

  constructor(public router: Router, public teamSrv: TeamsService, public authSrv: AuthService, private readonly pigeonSrv: PigeonService) {
    super(router, teamSrv, authSrv);
  }

  teamToDelete: FormulaTeam;
  deleteText: string;
  ngOnInit(): void {
    this.pigeonSrv.getCurrentData
    .subscribe(jsonData => {
        this.teamToDelete = JSON.parse(jsonData);
    });
    this.deleteText = `${this.teamToDelete.name} (${this.teamToDelete.numberOfChampionshipsWon})
    from ${this.teamToDelete.yearOfFoundation}`;
  }

  onSubmit() {
    this.teamSrv.deleteElement(this.teamToDelete)
    .subscribe(
        data => {
            this.redirectTo('me');
          });
  }

  onCancel() {
    this.redirectTo('me');
  }

}
