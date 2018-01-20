import { Component, Inject } from '@angular/core';
import { Http, RequestOptions } from '@angular/http';

@Component({
    selector: 'tasks',
    templateUrl: './tasks.component.html'
})
export class TasksComponent {
    public tasks: TasksDisplay[];

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        http.get(baseUrl + 'api/Tasks/TasksDisplays').subscribe(result => {
            this.tasks = result.json() as TasksDisplay[];
        }, error => console.error(error));
    }

    public delete(task: TasksDisplay) {
        this.http.post(this.baseUrl + 'api/Tasks/DeleteTask', task).subscribe(result => {
            if (result) {
                //this.router.navigate(['/tasks']);
            }
        }, error => {
            console.error(error);
        });
    }
}

interface TasksDisplay {
    id: string;
    dueDate: string;
    title: string;
    details: string;
    creationDate: string;
    users: string;
}
