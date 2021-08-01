import React from 'react';
import * as moment from 'moment';
import { makeStyles } from '@material-ui/core/styles';
import clsx from 'clsx';
import Card from '@material-ui/core/Card';
import CardHeader from '@material-ui/core/CardHeader';
import CardMedia from '@material-ui/core/CardMedia';
import CardContent from '@material-ui/core/CardContent';
import CardActions from '@material-ui/core/CardActions';
import Collapse from '@material-ui/core/Collapse';
import Avatar from '@material-ui/core/Avatar';
import IconButton from '@material-ui/core/IconButton';
import Typography from '@material-ui/core/Typography';
import { red } from '@material-ui/core/colors';
import FavoriteIcon from '@material-ui/icons/Favorite';
import ShareIcon from '@material-ui/icons/Share';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import MoreVertIcon from '@material-ui/icons/MoreVert';
import { Article } from '../Classes/Article';
import { IsNullOrEmpty } from '../Utilities/CommonUtilities';

interface IArticleCardProps {
    article: Article
}

const useStyles = makeStyles((theme) => ({
  root: {
    maxWidth: 345,
  },
  media: {
    height: 0,
    paddingTop: '56.25%', // 16:9
  },
  expand: {
    transform: 'rotate(0deg)',
    marginLeft: 'auto',
    transition: theme.transitions.create('transform', {
      duration: theme.transitions.duration.shortest,
    }),
  },
  expandOpen: {
    transform: 'rotate(180deg)',
  },
  favoriteUnclicked: {
    color: '',
  },
  favoriteClicked: {
    color: 'secondary',
  },
}));

export default function ArticleCard(props: IArticleCardProps) {
  const classes = useStyles();
  const [expanded, setExpanded] = React.useState(false);
  const [isFavorite, setIsFavorite] = React.useState(false);

  console.dir(props.article);

  const handleExpandClick = () => {
    setExpanded(!expanded);
  };

  const handleFavoriteClick = () => {
    setIsFavorite(!isFavorite);
  }

  let dateString = moment(props.article.datePublished).format("MM/DD/YYYY");

  return (
    <div style={{ margin: 10 }}>
      <Card className={classes.root} variant="outlined">
        <CardHeader title={props.article.articleTitle} subheader={"By " + props.article.articleAuthor + " - " + dateString}/>
        <CardMedia className={classes.media} title={props.article.articleTitle} image={props.article.imageUrl}/>
        <CardContent>
          <Typography variant="body2" color="textSecondary" component="p">
            {props.article.articleDescription}
          </Typography>
        </CardContent>
        <CardActions disableSpacing>
          <IconButton aria-label="add to favorites" className={clsx(classes.favoriteUnclicked, {
              [classes.favoriteClicked]: isFavorite,
            })}
            onClick={handleFavoriteClick}>
            <FavoriteIcon />
          </IconButton>
          {!IsNullOrEmpty(props.article.additionalDescription) &&
          <IconButton className={clsx(classes.expand, {
              [classes.expandOpen]: expanded,
            })} 
            onClick={handleExpandClick}
            aria-expanded={expanded}
            aria-label="show more">
          <ExpandMoreIcon />
        </IconButton>}
        </CardActions>
        <Collapse in={expanded} timeout="auto" unmountOnExit>
          <CardContent>
            <Typography paragraph>Additional Information:</Typography>
            <Typography paragraph>{props.article.additionalDescription}</Typography>
          </CardContent>
        </Collapse>
      </Card>
    </div>
  );
}