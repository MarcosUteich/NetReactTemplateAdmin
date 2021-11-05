import React, { useState } from 'react';
import { useHistory } from 'react-router-dom';

import {
    TextField,
    IconButton,
    Button,
    Grid,
    Divider,
    TableBody,
    TableContainer,
    TableHead,
    Table,
    TableRow,
    Modal,
    Paper,
    Typography,
    Autocomplete,
    Box
} from '@material-ui/core';

import TableCell, { tableCellClasses } from '@material-ui/core/TableCell';

import {
    AddCircleOutlineOutlined,
    RemoveCircleOutline
} from '@material-ui/icons';

import { makeStyles, styled } from '@material-ui/styles';

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

const StyledTableCell = styled(TableCell)(({ theme }) => ({
    [`&.${tableCellClasses.head}`]: {
        backgroundColor: '#e3f2fd',
        color: theme.palette.common.black,
    },
    [`&.${tableCellClasses.body}`]: {
        fontSize: 14,
    },
}));

const StyledTableRow = styled(TableRow)(({ theme }) => ({
    '&:nth-of-type(odd)': {
        backgroundColor: theme.palette.action.hover,
    },
    // hide last border
    '&:last-child td, &:last-child th': {
        border: 0,
    },
}));

const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 500,
    bgcolor: 'background.paper',
    border: '1px solid #000',
    boxShadow: 24,
    p: 4,
};

const OrderEditView = (props) => {
    console.log(props.location.state)
    const [order, setOrder] = useState(props.location.state);
    const [products, setProducts] = useState([{}]);
    const [status, setStatus] = useState([{}]);
    const [customers, setCustomers] = useState([{}]);
    const [open, setOpen] = React.useState(false);
    const [productSelected, setProductSelected] = React.useState({});
    const classes = useStyles();
    const history = useHistory();

    React.useEffect(() => {
        getProducts();
        getStatus();
        getCustomers();
    }, [])

    function ccyFormat(num) {
        return `${num.toFixed(2)}`;
    }

    const removeProduct = (id) => {
        const newList = order.products.filter((item) => item.id !== id);
        setOrder({ ...order, products: newList })
    }

    function getSelectedItem() {
        const found = customers.find(country => country.id === '6164ea8dff1040fa0ad187e8')
        return found || {};
    }

    const getCustomers = () => {
        fetch('order/getCustomers')
            .then(response => response.json())
            .then(response => {
                setCustomers(response);
            })
            .catch(error => setOrder([]))
    }

    const getProducts = () => {
        fetch('order/getProducts')
            .then(response => response.json())
            .then(response => {
                setProducts(response);
            })
            .catch(error => setOrders([]))
    }

    const getStatus = () => {
        fetch('order/getStatus')
            .then(response => response.json())
            .then(response => {
                setStatus(response);
            })
            .catch(error => setOrders([]))
    }

    const save = () => {
        fetch('order/updateOrder', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(order)
        })
            .then(response => history.push("/order"))
            .catch(error => { });
    }
    
    return (
        <MainCard title='Edit Order'>

            <Modal
                open={open}
                onClose={() => setOpen(false)}
                aria-labelledby="modal-modal-title"
                aria-describedby="modal-modal-description"
            >
                <Box sx={style}>
                    <Typography variant="h3">Add product to order</Typography>
                    <Divider className={classes.dividerGrade} />
                    <Grid container className={classes.grid}>
                        <Grid item lg={8} md={8} sm={8}>
                            <Autocomplete
                                options={products}
                                getOptionLabel={(option) => option.name}
                                renderInput={(params) => <TextField {...params} label="Product" />}
                                onChange={(event, item) => {
                                    setProductSelected(item);
                                }}
                            />
                        </Grid>
                        <Grid item lg={1} md={1} sm={1} />
                        <Grid item lg={3} md={3} sm={3}>
                            <TextField
                                required
                                value={productSelected.price}
                                label="Price (R$)"
                                name="Price" />
                        </Grid>
                    </Grid>
                    <Divider className={classes.dividerGrade} />
                    <Grid container className={classes.grid}>
                        <Grid item lg={7} md={7} sm={7} />
                        <Grid item lg={5} md={5} sm={5}>
                            <Button variant="contained" color="info" onClick={e => { setOpen(false) }}>
                                Cancel
                            </Button>
                            <Button variant="contained" color="success" onClick={(event) => {
                                order.products.push(productSelected);
                                setOpen(false);
                            }} className={classes.buttonSave}>
                                Save
                            </Button>
                        </Grid>
                    </Grid>
                </Box>
            </Modal>

            <Grid container className={classes.grid}>
                <Grid item lg={3} md={3} sm={3}>
                    <Autocomplete
                        name="status"
                        options={status}
                        getOptionLabel={(option) => option.name}
                        renderInput={(params) => <TextField {...params} label="Status" />}
                        onChange={(event, item) => {
                            setOrder({
                                ...order,
                                ["status"]: item.id
                            })
                        }} />
                </Grid>
                <Grid item lg={1} md={1} sm={1} />
                <Grid item lg={5} md={5} sm={5}>
                    <Autocomplete
                        name="customerId"
                        options={customers}
                        getOptionLabel={(option) => option.name}
                        renderInput={(params) => <TextField {...params} label="Customer" />}
                        onChange={(event, item) => {
                            setOrder({
                                ...order,
                                ["customerId"]: item.id
                            })
                        }} />
                </Grid>
                <Grid item lg={3} md={3} sm={3} />
            </Grid>

            <Divider className={classes.dividerGrade} />

            <Grid container className={classes.grid}>
                <Grid item lg={12} md={12} sm={12}>
                    <Typography variant="h5" gutterBottom component="div">
                        List of Products
                    </Typography>
                    <TableContainer component={Paper}>
                        <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
                            <TableHead>
                                <TableRow>
                                    <StyledTableCell align="left">Name</StyledTableCell>
                                    <StyledTableCell align="left">Price</StyledTableCell>
                                    <StyledTableCell align="center">
                                        <IconButton color="primary" onClick={() => setOpen(true)}>
                                            <AddCircleOutlineOutlined />
                                        </IconButton>
                                    </StyledTableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {order.products && order.products.map((product) => (
                                    <TableRow key={product.id} sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
                                        <StyledTableCell align="left">{product.name}</StyledTableCell>
                                        <StyledTableCell align="left">{product.price}</StyledTableCell>
                                        <StyledTableCell align="center">
                                            <IconButton color="primary" onClick={() => { removeProduct(product.id) }} >
                                                <RemoveCircleOutline />
                                            </IconButton>
                                        </StyledTableCell>
                                    </TableRow>
                                ))}
                                {order.products.length > 0 &&
                                    <TableRow>
                                        <StyledTableCell>Subtotal</StyledTableCell>
                                        <StyledTableCell align="left">{ccyFormat(order.products.reduce((a, v) => a = a + v.price, 0))}</StyledTableCell>
                                    </TableRow>
                                }
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Grid>
            </Grid>

            <Divider className={classes.dividerGrade} />

            <Grid container>
                <Grid item lg={10} md={10} sm={10} />
                <Grid item lg={2} md={2} sm={2}>
                    <Button variant="contained" color="info" onClick={e => { history.push("/order") }}>
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

export default OrderEditView;
