import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { Routes,RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { AppComponent } from './app.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule, MatButtonModule, MatTabsModule, MatSelectModule, MatCardModule } from '@angular/material';
import { MatRadioModule, MatMenuModule, MatProgressBarModule, MatSidenavModule } from '@angular/material';
import { MatDialogModule, MatIconModule } from '@angular/material'; 
import { MatPaginatorModule } from '@angular/material';
import 'hammerjs';

import { ApiService } from './services/api.service';

import { LoginComponent } from './login/login.component';
import { CadastroComponent } from './cadastro/cadastro.component';
import { AuthGuardService } from './services/auth-guard.service';
import { AuthService } from './services/auth.service';

const rotas: Routes = [
  { path: '', component: LoginComponent, pathMatch: "full" },
  { path: 'Admin', canActivate:[AuthGuardService], loadChildren: 'app/adm/adm.module#AdmModule'}, // Lazy loading
  { path: 'Login', component: LoginComponent },
  { path: 'Cadastro', component: CadastroComponent },
];


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    CadastroComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,  MatButtonModule, MatSelectModule,
    MatTabsModule, MatInputModule, MatCardModule,  MatSidenavModule,
    MatRadioModule, MatMenuModule, MatProgressBarModule, MatDialogModule,
    MatIconModule, MatPaginatorModule,
    RouterModule.forRoot(rotas)
  ],
  providers: [ApiService, AuthGuardService, AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
