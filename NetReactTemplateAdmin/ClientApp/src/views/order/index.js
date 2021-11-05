import {
    Grid, IconButton, InputAdornment, OutlinedInput, Paper, Table,
    TableBody,
    TableContainer,
    TableHead,
    TableRow,
    Collapse,
    Box,
    Typography
} from '@material-ui/core';

import TableCell, { tableCellClasses } from '@material-ui/core/TableCell';

import {
    AddCircleOutlineOutlined,
    EditOutlined,
    KeyboardArrowDown as KeyboardArrowDownIcon,
    KeyboardArrowUp as KeyboardArrowUpIcon
} from '@material-ui/icons';

import { makeStyles, styled } from '@material-ui/styles';
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

function Row(props) {
    const { row } = props;
    const [open, setOpen] = React.useState(false);
    const history = useHistory();

    return (
        <React.Fragment>
            <StyledTableRow
                key={row.id}
                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                onClick={() => setOpen(!open)}
            >
                <StyledTableCell>
                    <IconButton
                        aria-label="expand row"
                        size="small"
                    >
                        {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
                    </IconButton>
                </StyledTableCell>
                <StyledTableCell align="left">{row.status}</StyledTableCell>
                <StyledTableCell align="left">{row.customer}</StyledTableCell>
                <StyledTableCell align="left">{row.total}</StyledTableCell>
                <StyledTableCell align="center">
                    <IconButton color="primary" onClick={() => { history.push("/order-edit", row) }}>
                        <EditOutlined />
                    </IconButton>
                </StyledTableCell>
            </StyledTableRow>
            <TableRow>
                <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
                    <Collapse in={open} timeout="auto" unmountOnExit>
                        <Box sx={{ margin: 1 }}>
                            <Typography variant="h5" gutterBottom component="div">
                                Products
                            </Typography>
                            <Table size="small" aria-label="purchases">
                                <TableHead>
                                    <TableRow>
                                        <TableCell>Product</TableCell>
                                        <TableCell align="right">Price (R$)</TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {row.products.map((historyRow) => (
                                        <TableRow key={historyRow.id}>
                                            <TableCell>{historyRow.name}</TableCell>
                                            <TableCell align="right">{historyRow.price}</TableCell>
                                        </TableRow>
                                    ))}
                                </TableBody>
                            </Table>
                        </Box>
                    </Collapse>
                </TableCell>
            </TableRow>
        </React.Fragment>
    );
}

const OrderView = () => {
    const classes = useStyles();
    const history = useHistory();

    const [orders, setOrders] = useState([]);

    const orderNew = () => {
        history.push("/order-new");
    }

    React.useEffect(() => {
        getOrders();
    }, [])

    const getOrders = () => {
        fetch('order/GetOrders')
            .then(response => response.json())
            .then(response => {
                setOrders(response)
            })
            .catch(error => setOrders([]))
    }

    return (
        <MainCard title="Orders">
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
                    <TableHead>
                        <TableRow>
                            <StyledTableCell align="left"></StyledTableCell>
                            <StyledTableCell align="left">Status</StyledTableCell>
                            <StyledTableCell align="left">Customer</StyledTableCell>
                            <StyledTableCell align="left">Total (R$)</StyledTableCell>
                            <StyledTableCell align="center">
                                <IconButton color="primary" onClick={orderNew}>
                                    <AddCircleOutlineOutlined />
                                </IconButton>
                            </StyledTableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {orders.map((order) => (
                            <Row key={order.id} row={order} />
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </MainCard>
    );
};

export default OrderView;
