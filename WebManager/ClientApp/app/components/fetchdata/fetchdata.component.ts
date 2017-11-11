import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html'
})
export class FetchDataComponent {
    public tasks: TasksDisplay[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/SampleData/TasksDisplays').subscribe(result => {
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
