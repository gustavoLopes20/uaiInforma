import { NgModule} from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes,RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import {AdmComponent} from './adm.component';

//angular material
import { MatInputModule, MatButtonModule, MatTabsModule, MatSelectModule, MatCardModule } from '@angular/material';
import { MatRadioModule, MatMenuModule, MatProgressBarModule, MatSidenavModule } from '@angular/material';
import { MatDialogModule, MatIconModule } from '@angular/material'; 
import { MatPaginatorModule } from '@angular/material';
import 'hammerjs';

import { UnidadesAtendimentoComponent } from './unidades-atendimento/unidades-atendimento.component';
import { SetoresComponent } from './setores/setores.component';
import { MedicosComponent } from './medicos/medicos.component';
import { EscalasComponent } from './escalas/escalas.component';
import { EspecialidadesComponent } from './especialidades/especialidades.component';
import { EscalaComponent } from './escalas/escala/escala.component';
import { AuthGuardService } from '../services/auth-guard.service';
import { ApiService } from '../services/api.service';
import { AuthService } from '../services/auth.service';

const rotas:Routes  = [
  {path : '', canActivate:[AuthGuardService], component: AdmComponent, children: [
     { path : 'UnidadedesDeAtentimento', component: UnidadesAtendimentoComponent },
     { path : 'Setores', component: SetoresComponent },
     { path : 'Medicos', component: MedicosComponent },
     { path : 'Escalas', component: EscalasComponent},
     { path : 'Escala/:escala', component: EscalaComponent },
     { path : 'Especialidades', component: EspecialidadesComponent },
  ] } 
];

@NgModule({
    imports: [
      CommonModule,
      FormsModule,
      HttpModule,
      ReactiveFormsModule,
      MatButtonModule, MatSelectModule,
      MatTabsModule, MatInputModule, MatCardModule,  MatSidenavModule,
      MatRadioModule, MatMenuModule, MatProgressBarModule, MatDialogModule,
      MatIconModule, MatPaginatorModule,	
      RouterModule.forChild(rotas),
    ],
    declarations: [
      AdmComponent,
      UnidadesAtendimentoComponent,
      SetoresComponent,
      MedicosComponent,
      EscalasComponent,
      EspecialidadesComponent,
      EscalaComponent
    ],
    providers: [AuthGuardService, ApiService, AuthService],  
    bootstrap: [AdmComponent]
})
export class AdmModule {}