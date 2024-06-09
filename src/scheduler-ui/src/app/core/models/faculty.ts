import { Teacher } from './teacher';
import { TimeTable } from './timetable';

export interface Faculty {
    id: string;
    name: string;
    description: string;
    address: string;
    dean: Teacher;
    timeTable: TimeTable;
}
