import {
    Grid, IconButton, InputAdornment, OutlinedInput, Paper, Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow
} from '@material-ui/core';
import {
    AddCircleOutlineOutlined,
    EditOutlined
} from '@material-ui/icons';
import { makeStyles } from '@material-ui/styles';
import { IconSearch } from '@tabler/icons';
import React, { useState } from 'react';
import { useHistory } from 'react-router-dom';
import MainCard from '../../ui-component/cards/MainCard';

const useStyles = makeStyles(theme => ({
    searchControl: {
        width: '100%',
        paddingRight: '8px',
        paddingLeft: '16px',
        marginBottom: '16px',
        marginTop: '16px'
    }
}));

const CustomerView = () => {
    const classes = useStyles();
    const history = useHistory();

    const [customers, setCustomers] = useState([{}]);

    const addCustomer = () => {
        history.push("/customer-new");
    }

    const editCustomer = (customer) => {
        history.push("/customer-edit", customer);
    }

    const searchCustomers = (text) => {
        if (!text) {
            getCustomers();
        } else {
            fetch('customer/GetCustomersByName?name=' + text)
                .then(response => response.json())
                .then(response => {
                    setCustomers(response);
                })
                .catch(error => setCustomers([]))
        }
    }

    React.useEffect(() => {
        getCustomers();
    }, [])

    const getCustomers = () => {
        fetch('customer/GetCustomers')
            .then(response => response.json())
            .then(response => {
                setCustomers(response);
            })
            .catch(error => setCustomers([]))
    }

    return (
        <MainCard title="Customer List">

            <Grid container className={classes.grid}>
                <Grid item lg={4} md={4} sm={4}>
                    <OutlinedInput
                        className={classes.searchControl}
                        id="input-search-profile"
                        onChange={(e) => searchCustomers(e.target.value)}
                        placeholder="Search by name"
                        startAdornment={
                            <InputAdornment position="start">
                                <IconSearch stroke={1.5} size="1.3rem" className={classes.startAdornment} />
                            </InputAdornment>
                        }
                        aria-describedby="search-helper-text"
                        inputProps={{
                            'aria-label': 'weight'
                        }}
                    />
                </Grid>
                <Grid item lg={8} md={8} sm={8}/>
            </Grid>
            
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
                    <TableHead>
                        <TableRow>
                            <TableCell align="left">Name</TableCell>
                            <TableCell align="left">Email</TableCell>
                            <TableCell align="left">Active</TableCell>
                            <TableCell align="left">Date Created</TableCell>
                            <TableCell align="center">
                                <IconButton color="primary" onClick={addCustomer}>
                                    <AddCircleOutlineOutlined />
                                </IconButton>
                            </TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {customers.map((customer) => (
                            <TableRow key={customer.id} sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
                                <TableCell align="left">{customer.name}</TableCell>
                                <TableCell align="left">{customer.email}</TableCell>
                                <TableCell align="left">{customer.active ? 'Yes' : 'No'}</TableCell>
                                <TableCell align="left">{customer.dateCreated}</TableCell>
                                <TableCell align="center">
                                    <IconButton color="primary" onClick={() => editCustomer(customer)}>
                                        <EditOutlined />
                                    </IconButton>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </MainCard>
    );
};

export default CustomerView;
