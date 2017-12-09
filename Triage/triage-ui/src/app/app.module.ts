import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NbThemeModule } from '@nebular/theme';
import { DashModule } from './dash/dash.module';

import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    DashModule,
    NbThemeModule.forRoot({name: 'default'})
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
