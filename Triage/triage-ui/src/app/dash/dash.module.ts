import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NbSidebarModule, NbLayoutModule, NbSidebarService } from '@nebular/theme';

import { DashRoutingModule } from './dash-routing.module';
import { DashComponent } from './dash.component';


@NgModule({
  imports: [
    CommonModule,
    DashRoutingModule,
    NbLayoutModule,
    NbSidebarModule
  ],
  providers: [NbSidebarService],
  declarations: [DashComponent],
  exports: [DashComponent]
})
export class DashModule { }
