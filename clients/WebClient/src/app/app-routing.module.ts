import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DesktopComponent } from './Desktop/desktop.component';
import { PageNotFoundComponent } from './Standard/PageNotFound/PageNotFound.component';
import { WorksComponent } from './Works/works.component';
import { WorkSettingsComponent } from './Works/WorkSettings/WorkSettings.component';

const routes: Routes = [
     {
          path: 'works',
          component: WorksComponent
     },
     {
          path: 'desktop/:id',
          component: DesktopComponent
     },
     {
          path: 'settings/:id',
          component: WorkSettingsComponent
     },
     /*      {
               path: 'heroes',
               component: DesktopComponent,
               data: { title: 'Heroes List' }
          }, */
     {
          path: '**',
          component: PageNotFoundComponent
     }
];

@NgModule({
     imports: [RouterModule.forRoot(routes)],
     exports: [RouterModule]
})
export class AppRoutingModule { }
