import React, { useRef } from 'react';
import '../custom.css';
import { makeStyles, formatMs } from '@material-ui/core/styles';
import InputLabel from '@material-ui/core/InputLabel';
import TextField from '@material-ui/core/TextField';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import CompareArrowsIcon from '@material-ui/icons/CompareArrows';
import Button from '@material-ui/core/Button';
import axios from 'axios';
const useStyles = makeStyles((theme) => ({
    formControl: {
        margin: theme.spacing(1),
        minWidth: 120,
    },
    selectEmpty: {
        marginTop: theme.spacing(2),
    },
}));
let pending = true;
var dropDownsData = [];
function idOf(val) {
    for (let i = 0; i < dropDownsData.length; i++) {
        if (dropDownsData[i].name === val)
            return dropDownsData[i].id;
	}
}
//todo: clean code
//todo: make the reverse icon tappable
export function Home() {
    const classes = useStyles();
    var [data, setData] = React.useState(0);
    if (pending || dropDownsData.length === 0) {
        axios.get(window.location.origin+'/api/Exchanges/DropDownData_Currency').then(function (response) {
            pending = false;
            dropDownsData = response.data;
            setData(1);
        })
    }
    var convertedValue;
    function exchangeBtnhandle(e) {
        var model = {
            from_currency_id: idOf(document.getElementById('fromSelect').value),
            to_currency_id: idOf(document.getElementById('toSelect').value),
            value: parseFloat(convertedValue)
        }
        axios.post(window.location.origin+'/api/Exchanges/Exchange/', model).then(function (response) {
            document.getElementById('result').value = response.data;
        });
    }
    function onFromValueChange(e) {
        convertedValue = e.target.value;
    }
    function swapCurrencies(e) {
        var temporary = document.getElementById('fromSelect').value;
        document.getElementById('fromSelect').value = document.getElementById('toSelect').value
        document.getElementById('toSelect').value = temporary;
    }
    return (
        <div className='converter-page'>
            <h1>Money Exchange</h1>
            <div>
                <TextField label="FROM value" variant="outlined" onChange={onFromValueChange}/>
                <FormControl variant="outlined" className={classes.formControl}
                    style={{ margin: "0", }}>
                    <Select
                    native
                    label="From"
                        inputProps={{ name: 'currency1', id: 'fromSelect', }}
                       
                    >
                        {
                            pending ? <option  value={1} key={1}/> :
                                dropDownsData.map(function (element) {
                                    return <option value={element.Id} key={element.Id}>{element.name}</option>
                                })
                        }
                        
                </Select>
            </FormControl>
            
                <CompareArrowsIcon style={{ fontSize: 60, textAlign: "center", display: "inline-block", paddingRight: "25px", }}
                    onClick={swapCurrencies}/>
           
                <TextField label="" variant="outlined" id='result'/>
                <FormControl variant="outlined" className={classes.formControl} style={{margin: "0",}}>
                    <Select
                        native
                        label="To"
                        inputProps={{
                            name: 'currency2',
                            id: 'toSelect',
                        }}>
                        {
                            pending ? <option  value={1} key={1}/> :
                                dropDownsData.map(function (element) {
                                    return <option value={element.Id} key={element.Id}>{element.name}</option>
                                })
                        }
                    </Select>
                </FormControl>
            </div>
            <Button variant="contained" className="exchange-button" onClick={exchangeBtnhandle}>Exchange</Button>
      </div>
    );
}
