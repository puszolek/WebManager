import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { TasksComponent } from './components/tasks/tasks.component';
import { TasksAddNewComponent } from './components/tasks/tasks.addNew.component';
import { LogoutComponent } from './components/logout/logout.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        LogoutComponent,
        TasksComponent,
        HomeComponent,
        TasksAddNewComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'logout', component: LogoutComponent },
            { path: 'tasks', component: TasksComponent },
            { path: 'tasks/new', component: TasksAddNewComponent},
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
