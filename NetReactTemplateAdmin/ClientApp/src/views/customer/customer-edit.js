import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';

import {
    TextField,
    Button,
    Grid,
    Divider,
    Checkbox
} from '@material-ui/core';

import { makeStyles } from '@material-ui/styles';

import MainCard from '../../ui-component/cards/MainCard';

const useStyles = makeStyles(theme => ({
    dividerGrade: {
        marginTop: '25px',
        marginBottom: '25px'
    },
    grid: {
        paddingLeft: '24px'
    },
    buttonSave: {
        marginLeft: '10px'
    }
}));

const CustomerEditView = (props) => {
    const [state, setState] = useState(props.location.state);
    const classes = useStyles();
    const history = useHistory();

    const handleChange = (event) => {
        const { name, value, checked } = event.target;
        state.Active = true;
        setState({
            ...state,
            [name]: name == 'Active'
                ? checked
                : value
        });
    };

    const save = () => {
        fetch('customer/updateCustomer', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(state)
        })
            .then(response => history.push("/customer"))
            .catch(error => { });
    }

    return (
        <MainCard title='Customer Edit'>
            <Grid container className={classes.grid}>
                <Grid item lg={3} md={3} sm={3}>
                    <TextField
                        label="Name"
                        name="Name"
                        defaultValue={state.name}
                        onChange={handleChange} />
                </Grid>
                <Grid item lg={3} md={3} sm={3}>
                    <TextField
                        required
                        id="outlined-required"
                        label="Email"
                        name="Email"
                        defaultValue={state.email}
                        onChange={handleChange} />
                </Grid>
                <Grid item lg={3} md={3} sm={3}>
                    <Checkbox
                        name="Active"
                        defaultChecked={state.active}
                        onChange={handleChange} />
                </Grid>
            </Grid>

            <Divider className={classes.dividerGrade} />

            <Grid container>
                <Grid item lg={10} md={10} sm={10} />
                <Grid item lg={2} md={2} sm={2}>
                    <Button variant="contained" color="info" onClick={e => { history.push("/customer") }}>
                        Cancel
                    </Button>
                    <Button variant="contained" color="success" onClick={save} className={classes.buttonSave}>
                        Save
                    </Button>
                </Grid>
            </Grid>

        </MainCard>
    );
};

export default CustomerEditView;
