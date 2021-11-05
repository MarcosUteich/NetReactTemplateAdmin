import React, { useState } from 'react';
import { useHistory } from 'react-router-dom';

import {
    TextField,
    Button,
    Grid,
    Divider,
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

const ProducEditView = (props) => {
    const [state, setState] = useState(props.location.state);
    const classes = useStyles();
    const history = useHistory();

    const handleChange = (event) => {
        const { name, value } = event.target;
        setState({
            ...state,
            [name]: value
        });
    };

    const save = () => {
        fetch('product/updateProduct', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(state)
        })
            .then(response => history.push("/product"))
            .catch(error => { });
    }

    return (
        <MainCard title='Product Edit'>
            <Grid container className={classes.grid}>
                <Grid item lg={4} md={4} sm={4}>
                    <TextField
                        required
                        id="outlined-required"
                        label="Name"
                        name="Name"
                        defaultValue={state.name}
                        onChange={handleChange} />
                </Grid>              
                <Grid item lg={4} md={4} sm={4}>
                    <TextField
                        required
                        id="outlined-required"
                        label="Price R$"
                        name="Price"
                        defaultValue={state.price}
                        onChange={handleChange} />
                </Grid>
                <Grid item lg={4} md={4} sm={4}/>
            </Grid>

            <Divider className={classes.dividerGrade} />

            <Grid container>
                <Grid item lg={10} md={10} sm={10} />
                <Grid item lg={2} md={2} sm={2}>
                    <Button variant="contained" color="info" onClick={e => { history.push("/product") }}>
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

export default ProducEditView;
