import React from 'react';

// material-ui
import { useTheme } from '@material-ui/styles';

import {
    IconButton
} from '@material-ui/core';

import {
    Museum as MuseumIcon
} from '@material-ui/icons';

/**
 * if you want to use image instead of <svg> uncomment following.
 *
 * import logoDark from './../../assets/images/logo-dark.svg';
 * import logo from './../../assets/images/logo.svg';
 *
 */

//-----------------------|| LOGO SVG ||-----------------------//

const Logo = () => {
    const theme = useTheme();

    return (
        /**
         * if you want to use image instead of svg uncomment following, and comment out <svg> element.
         *
         * <img src={logo} alt="Berry" width="100" />
         *
         */
        <React.Fragment>
            <IconButton color="primary" onClick={() => editData(data)}>
                <MuseumIcon />
            </IconButton>

            <h3>WEBSITE</h3>
        </React.Fragment>
    );
};

export default Logo;
