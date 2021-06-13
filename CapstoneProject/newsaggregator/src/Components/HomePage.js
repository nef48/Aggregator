import * as React from 'react';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/MenuIcon';

function HomePage() {


    return (
        <div>
            <React.Fragment>
                <AppBar position="fixed">
                    <Toolbar>
                        <IconButton
                            edge="start"
                            className={classes.menuButton}
                            color="inherit"
                            aria-label="open drawer">
                            <MenuIcon />
                        </IconButton>
                    </Toolbar>
                </AppBar>
            </React.Fragment>
        </div>
    )
}

export default HomePage;