import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'tasks',
    templateUrl: './tasks.component.html'
})
export class TasksComponent {
    public tasks: TasksDisplay[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/Tasks/TasksDisplays').subscribe(result => {
            this.tasks = result.json() as TasksDisplay[];
        }, error => console.error(error));
    }
}

interface TasksDisplay {
    dueDate: string;
    title: string;
    details: string;
    creationDate: string;
    users: string;
}
