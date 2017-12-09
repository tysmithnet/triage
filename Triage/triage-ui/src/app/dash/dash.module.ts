import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashRoutingModule } from './dash-routing.module';
import { DashComponent } from './dash.component';

@NgModule({
  imports: [
    CommonModule,
    DashRoutingModule
  ],
  declarations: [DashComponent],
  exports: [DashComponent]
})
export class DashModule { }
