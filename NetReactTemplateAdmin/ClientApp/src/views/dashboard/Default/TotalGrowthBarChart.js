import PropTypes from 'prop-types';
import React from 'react';

// material-ui
import { Grid, MenuItem, TextField, Typography, useTheme } from '@material-ui/core';

// third-party
import ApexCharts from 'apexcharts';
import Chart from 'react-apexcharts';

// project imports
import SkeletonTotalGrowthBarChart from './../../../ui-component/cards/Skeleton/TotalGrowthBarChart';
import MainCard from './../../../ui-component/cards/MainCard';
import { gridSpacing } from './../../../store/constant';

//-----------------------|| DASHBOARD DEFAULT - TOTAL GROWTH BAR CHART ||-----------------------//

const TotalGrowthBarChart = ({ isLoading }) => {
    const [value, setValue] = React.useState('today');
    const [chartData, setChartData] = React.useState({ loading: true });
    const [dashboardStatus, setDashboardStatus] = React.useState([{}]);
    const [totalGrowth, setTotalGrowth] = React.useState(0);

    const theme = useTheme();

    const primary = theme.palette.text.primary;
    const grey200 = theme.palette.grey[200];

    const primary200 = theme.palette.primary[200];
    const primaryDark = theme.palette.primary.dark;
    const secondaryMain = theme.palette.secondary.main;
    const secondaryLight = theme.palette.secondary.light;
    const grey500 = theme.palette.grey[500];

    React.useEffect(() => {
        getTotalGrowthBarChart();
        getDashboardStatus();
        getTotalGrowth();

        const newChartData = {
            ...chartData.options,
            colors: [primary200, primaryDark, secondaryMain, secondaryLight],
            xaxis: {
                labels: {
                    style: {
                        colors: [primary, primary, primary, primary, primary, primary, primary, primary, primary, primary, primary, primary]
                    }
                }
            },
            yaxis: {
                labels: {
                    style: {
                        colors: [primary]
                    }
                }
            },
            grid: {
                borderColor: grey200
            },
            tooltip: {
                theme: 'light'
            },
            legend: {
                labels: {
                    colors: grey500
                }
            }
        };

        // do not load chart when loading
        if (!chartData.loading) {
            ApexCharts.exec(`bar-chart`, 'updateOptions', newChartData);
        }
    }, [primary200, primaryDark, secondaryMain, secondaryLight, primary, grey200, isLoading, grey500]);

    const getTotalGrowth = () => {
        fetch('dashboard/getTotalGrowth')
            .then(response => response.json())
            .then(response => setTotalGrowth(response))
            .catch(error => setTotalGrowth(0))
    };

    const getDashboardStatus = () => {
        fetch('dashboard/getDashboardStatus')
            .then(response => response.json())
            .then(response => setDashboardStatus(response))
            .catch(error => setDashboardStatus({}))
    };

    const getTotalGrowthBarChart = () => {
        setChartData({ loading: true });

        const chartData = {
            height: 480,
            type: 'bar',
            options: {
                chart: {
                    id: 'bar-chart',
                    stacked: true,
                    toolbar: {
                        show: true
                    },
                    zoom: {
                        enabled: true
                    }
                },
                responsive: [
                    {
                        breakpoint: 480,
                        options: {
                            legend: {
                                position: 'bottom',
                                offsetX: -10,
                                offsetY: 0
                            }
                        }
                    }
                ],
                plotOptions: {
                    bar: {
                        horizontal: false,
                        columnWidth: '50%'
                    }
                },
                xaxis: {
                    type: 'category',
                    categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
                },
                legend: {
                    show: true,
                    fontSize: '14px',
                    fontFamily: `'Roboto', sans-serif`,
                    position: 'bottom',
                    offsetX: 20,
                    labels: {
                        useSeriesColors: false
                    },
                    markers: {
                        width: 16,
                        height: 16,
                        radius: 5
                    },
                    itemMargin: {
                        horizontal: 15,
                        vertical: 8
                    }
                },
                fill: {
                    type: 'solid'
                },
                dataLabels: {
                    enabled: false
                },
                grid: {
                    show: true
                }
            },
            series: [{}]
        };

        fetch('dashboard/getTotalGrowthBarChart')
            .then(response => response.json())
            .then(response => {
                chartData.series = response;
                setChartData({
                    value: chartData,
                    loading: false
                });
            })
            .catch(error => setChartData({
                loading: false
            }));
    };

    return (
        <React.Fragment>
            {chartData.loading ? (
                <SkeletonTotalGrowthBarChart />
            ) : (
                <MainCard>
                    <Grid container spacing={gridSpacing}>
                        <Grid item xs={12}>
                            <Grid container alignItems="center" justifyContent="space-between">
                                <Grid item>
                                    <Grid container direction="column" spacing={1}>
                                        <Grid item>
                                            <Typography variant="subtitle2">Total Growth</Typography>
                                        </Grid>
                                        <Grid item>
                                            <Typography variant="h3">R$ {totalGrowth}</Typography>
                                        </Grid>
                                    </Grid>
                                </Grid>
                                <Grid item>
                                    <TextField
                                        id="standard-select-currency"
                                        select
                                        value={value}
                                        onChange={(e) => setValue(e.target.value)}
                                    >
                                        {dashboardStatus.map((option) => (
                                            <MenuItem key={option.value} value={option.value}>
                                                {option.label}
                                            </MenuItem>
                                        ))}
                                    </TextField>
                                </Grid>
                            </Grid>
                        </Grid>
                        <Grid item xs={12}>
                            <Chart {...chartData.value} />
                        </Grid>
                    </Grid>
                </MainCard>
            )}
        </React.Fragment>
    );
};

TotalGrowthBarChart.propTypes = {
    isLoading: PropTypes.bool
};

export default TotalGrowthBarChart;
