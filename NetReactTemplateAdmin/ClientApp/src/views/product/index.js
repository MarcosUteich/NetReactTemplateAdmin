import React, { useState } from 'react';
import { useHistory } from 'react-router-dom';

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

const ProductView = () => {
    const classes = useStyles();
    const history = useHistory();

    const [data, setData] = useState([]);

    const newData = () => {
        history.push("/product-new");
    }

    const editData = (obj) => {
        history.push("/product-edit", obj);
    }

    const searchProducts = (text) => {
        if (!text) {
            getProducts();
        } else {
            fetch('product/getProductsByName?name=' + text)
                .then(response => response.json())
                .then(response => {
                    setData(response);
                })
                .catch(error => setData([]))
        }
    }

    React.useEffect(() => {
        getProducts();
    }, [])

    const getProducts = () => {
        fetch('product/getProducts')
            .then(response => response.json())
            .then(response => {
                setData(response)
            })
            .catch(error => setData([]))
    }

    return (
        <MainCard title="Product List">

            <Grid container className={classes.grid}>
                <Grid item lg={4} md={4} sm={4}>
                    <OutlinedInput
                        className={classes.searchControl}
                        id="input-search-profile"
                        onChange={(e) => searchProducts(e.target.value)}
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
                            <TableCell align="left">Price R$</TableCell>
                            <TableCell align="center">
                                <IconButton color="primary" onClick={newData}>
                                    <AddCircleOutlineOutlined />
                                </IconButton>
                            </TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {data.map((data) => (
                            <TableRow
                                key={data.id}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <TableCell align="left">{data.name}</TableCell>
                                <TableCell align="left">{data.price}</TableCell>
                                <TableCell align="center">
                                    <IconButton color="primary" onClick={() => editData(data)}>
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

export default ProductView;
