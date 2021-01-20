import * as React from 'react';
import { DataGrid } from '@material-ui/data-grid';
import axios from 'axios';
const headCells = [
    {
        field: 'id',
        id: 'id',
        numeric: false,
        disablePadding: false,
        headerName: 'Id',
        width: 250,
        resizable: true
    },
    {
        field: 'date',
        id: 'date',
        numeric: false,
        disablePadding: true,
        headerName: 'Date',
        width: 150,
        resizable: true
    },
    {
        field: 'fromAmmount',
        id: 'fromAmmount',
        numeric: true,
        disablePadding: false,
        headerName: 'From Ammount',
        width: 100,
        resizable: true
    },
    {
        field: 'fromCurrency',
        id: 'fromCurrency',
        numeric: false,
        disablePadding: false,
        headerName: 'From Curdrency',
        width: 100,
        resizable: true
    },
    {
        field: 'toAmmount',
        id: 'toAmmount',
        numeric: true,
        disablePadding: false,
        headerName: 'To Ammount',
        width: 100,
        resizable: true
    },
    {
        field: 'toCurrency',
        id: 'toCurrency',
        numeric: false,
        disablePadding: false,
        headerName: 'To Currency',
        width: 100,
        resizable: true
    },
];

var loaded = false;
export class FetchData extends React.Component {
    constructor(props) {
        super(props);
        this.state = { data: [] };
        if (!loaded) {
            //the this variable in axios references the inner method data,not the compontnt 'this' variable
            var that = this;
            axios.get(window.location.origin+'/api/Exchanges/GetLogs').then(function (response) {
                that.setState({ data: response.data });
            });
            loaded = true;
        }
    }
    componentWillUnmount() {
        loaded = false;
	}
    render() {
        return (
            <div style={{ height: 500 }}>
                <DataGrid rows={this.state.data}
                    columns={headCells}
                    pageSize={this.state.data.length}
                    checkboxSelectiondisableColumnResize={false}
                    /*style={{ height: 500, width: 'auto' }}*//>
            </div>
        );
	}
}
