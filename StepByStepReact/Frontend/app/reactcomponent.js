import React, {Component} from 'react';
import Button from 'material-ui/Button';
import 'fullcalendar/dist/fullcalendar.css';
import 'fullcalendar/dist/fullcalendar.print.css';
import 'fullcalendar';
import 'fullcalendar-scheduler';

export default class Counter extends Component {
  constructor() {
    super();
    this.state = { currentCount: 0 };
   }
    incrementCounter() {
        this.setState({currentCount: this.state.currentCount + 1});
  }
    componentDidMount() {
        $('#calendar').fullCalendar({
            schedulerLicenseKey: 'CC-Attribution-NonCommercial-NoDerivatives'
        });
    }
  render() {
       return (
        <div>
            <h1>Counter</h1>
            <p>This is a simple example of a React component.</p>    
            <p>Current count: <strong>{this.state.currentCount}</strong></p>
            <Button onClick={() => { this.incrementCounter() }} raised color="primary">Increment</Button>
            <div id="calendar"></div>
        </div>
        );
      }
 
}