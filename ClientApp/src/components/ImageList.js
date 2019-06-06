import React, { Component } from 'react';
//import { Link as RouterLink } from 'react-router-dom';
import Link from '@material-ui/core/Link';
import GridList from '@material-ui/core/GridList';
import GridListTile from '@material-ui/core/GridListTile';
import GridListTileBar from '@material-ui/core/GridListTileBar';
import ListSubheader from '@material-ui/core/ListSubheader';
import IconButton from '@material-ui/core/IconButton';
import InfoIcon from '@material-ui/icons/Info';
import './ImageList.css';

export class ImageList extends Component {

  constructor(props) {
    super(props);
    this.state = { data: [], loading: true };

    fetch('api/FlickrData/Images')
      .then(response => response.json())
      .then(data => {
        this.setState({ data, loading: false });
      });
  }

  static renderImages(dataItems) {
    console.log(dataItems);

    return (
      <div className={"gridListContainer"}>
        <GridList cols={3}>
          <GridListTile key="Subheader" cols={3} className={"gridList"} style={{ height: 'auto' }}>
            <ListSubheader component="div">Flick images collected from https://www.flickr.com/services/feeds/</ListSubheader>
          </GridListTile>
          {dataItems.map((item, index) => (            
              <GridListTile key={index} cellheight="auto">
                  <img className={"tileImage"} src={item.media.m} alt={item.title} />
                <GridListTileBar
                  title={item.title}
                  subtitle={<span>{item.tags ? "tags:" + item.tags : ""}</span>}
                  actionIcon={
                    <IconButton aria-label={`info about ${item.title}`}>
                      <InfoIcon />
                    </IconButton>
                  }
                />
              </GridListTile>
          ))}
        </GridList>
      </div>);
  }

  render() {
    return (
      <div>
        {this.state.loading ? <p><em>Loading...</em></p> : ImageList.renderImages(this.state.data)}
      </div>
    );
  }
}
