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

const ProductNewView = () => {
    const classes = useStyles();
    const history = useHistory();
    const [state, setState] = useState({});

    const handleChange = (event) => {
        const { name, value, checked } = event.target;
        state.Active = true;
        setState({
            ...state,
            [name]: value
        });
    };

    const save = () => {
        fetch('product/AddProduct', {
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
                <Grid item lg={3} md={3} sm={3}/>
                <Grid item lg={3} md={3} sm={3}>
                    <TextField
                        required
                        id="outlined-required"
                        label="Name"
                        name="Name"
                        onChange={handleChange}
                    />
                </Grid>
                <Grid item lg={3} md={3} sm={3}>
                    <TextField
                        required
                        id="outlined-required"
                        label="Price"
                        name="Price"
                        onChange={handleChange}
                    />
                </Grid>
                <Grid item lg={3} md={3} sm={3}/>
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

export default ProductNewView;
