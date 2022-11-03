import React, {useContext, useEffect, useState} from 'react';
import {View, Text, FlatList, StyleSheet, ScrollView} from 'react-native';
import {CFetch} from '../../settings/APIFetch';
import {AppContext} from '../../settings/Context';
import Loader from '../../utilities/Loader';
import PostItem from './PostItem';
import RazorpayCheckout from 'react-native-razorpay';
import {Button} from 'react-native-paper';
import Colors from '../../settings/Colors';
import Snackbar from 'react-native-snackbar';
import FontAwesome from 'react-native-vector-icons/dist/FontAwesome';
var RNFS = require('react-native-fs');

const JobDetail = ({route}) => {
  const {appUser} = useContext(AppContext);
  const [loading, setLoading] = useState(true);
  const [waiting, setWaiting] = useState(false);
  const [item, setItem] = useState('');
  const [posts, setPosts] = useState([]);
  const [jobApply, setJobApply] = useState([]);
  const handleSelectPost = (postId, formFeeId) => {
    var array = [...jobApply];
    let same = array.filter(
      g => g.postId === postId && g.formFeeId === formFeeId,
    );
    if (same.length > 0) {
      let sIndex = array.indexOf(same[0]);
      if (sIndex > -1) {
        array.splice(sIndex, 1);
      }
      setJobApply(array);
      return false;
    }
    let filter = array.filter(g => g.postId === postId);
    let index = -1;
    if (filter.length > 0) {
      index = array.indexOf(filter[0]);
    }
    if (index > -1) {
      array.splice(index, 1);
    }
    array.push({postId: postId, formFeeId: formFeeId});
    setJobApply(array);
  };
  const loadData = () => {
    CFetch('JobDetail', appUser.token, {
      id: route.params.id,
    })
      .then(res => {
        if (res.status === 200) {
          res.json().then(result => {
            setItem(result.job);
            setPosts(result.posts);
          });
        }
      })
      .catch(error => {
        console.log(error);
      })
      .finally(() => {
        setLoading(false);
        setWaiting(false);
      });
  };

  useEffect(() => {
    loadData();
  }, []);

  // Payment Action Start //

  function CheckOut() {
    if (jobApply.length === 0) {
      Snackbar.show({
        text: 'Please select atleast one post for apply',
        duration: Snackbar.LENGTH_LONG,
      });
      return false;
    }
    setWaiting(true);
    setLoading(true);
    CFetch('InitiatePayment', appUser.token, jobApply)
      .then(res => {
        if (res.status == 200) {
          res
            .json()
            .then(result => {
              var options = {
                description: 'Paying to Quick App',
                image: 'https://quickapptechnologies.in/favicon.ico',
                currency: result.currency,
                key: result.razorpayKey,
                amount: result.amount,
                name: 'Quick App Technologies',
                order_id: result.orderId, //Replace this with an order_id created using Orders API. Learn more at https://razorpay.com/docs/api/orders.
                prefill: {
                  email: result.email,
                  contact: result.contactNumber,
                  name: result.name,
                },
                theme: {color: Colors.green},
              };
              RazorpayCheckout.open(options).then(data => {
                // handle success
                CFetch(
                  'CompletePayment?paymentId=' + data.razorpay_payment_id,
                  appUser.token,
                  jobApply,
                )
                  .then(res => {
                    if (res.status == 200) {
                      Toast.show('Payment Success');
                    } else if (res.status == 403) {
                      Toast.show('Please login before booking');
                      signOut();
                    } else if (res.status == 401) {
                      Toast.show('Payment Failed');
                    } else {
                      Toast.show('Something went wrong', Toast.SHORT);
                    }
                  })
                  .catch(error => {
                    console.log(error);
                  });
              });
            })
            .catch(error => {
              // handle failure
              Alert.alert('Payment Request Cancelled');
            });
        } else if (res.status == 403) {
          Alert.alert('Please login before booking');
          signOut();
        } else if (res.status === 441) {
          res.json().then(result => {
            setTimeout(() => {
              Snackbar.show({
                text: result,
                duration: Snackbar.LENGTH_LONG,
              });
            }, 100);
          });
        } else {
          setTimeout(() => {
            Snackbar.show({
              text: 'Something went wrong',
              duration: Snackbar.LENGTH_LONG,
            });
          }, 100);
        }
      })
      .catch(error => {
        console.log(error);
      })
      .finally(() => {
        setLoading(false);
        setWaiting(false);
      });
  }

  // Payment Action End //

  // File Download Start //

  const handlDownload = async url => {
    let index = url.lastIndexOf('/');
    let file = url.substring(index);
    if (file.length > 1) {
      RNFS.downloadFile({
        fromUrl: url,
        toFile:
          RNFS.DownloadDirectoryPath +
          '/' +
          url.substring(url.lastIndexOf('/')),
      })
        .promise.then(r => {
          if (r.statusCode === 200) {
            Snackbar.show({
              text: 'Downloaded Successfully',
              duration: Snackbar.LENGTH_LONG,
            });
          } else {
            Snackbar.show({
              text: 'Download failed try again',
              duration: Snackbar.LENGTH_LONG,
            });
          }
        })
        .catch(errr => {
          console.log(errr);
        });
    } else {
      Snackbar.show({
        text: 'No attachment found',
        duration: Snackbar.LENGTH_LONG,
      });
    }
  };

  // File Download End //

  return (
    <>
      <Loader loading={loading} waiting={waiting} spinner={true} />
      {item != '' && (
        <ScrollView>
          <View style={styles.container}>
            <View style={styles.detailscontainer}>
              <Text style={styles.jobtitle}>{item.jobName}</Text>
              <Text style={styles.jobcategory}>HSSC</Text>
            </View>
            <View style={styles.descriptioncontainer}>
              <Text style={styles.heading}>Job Description :</Text>
              <Text>{item.jobDescription}</Text>
              <Text style={styles.heading2}>Job Posts :</Text>
              <View style={styles.datecontainer}>
                <Text style={styles.datetitle}>Publish Date :- </Text>
                <Text style={styles.datetext}>{item.jobPublishDate}</Text>
              </View>
              <View style={styles.datecontainer}>
                <Text style={styles.datetitle}>Apply Date :- </Text>
                <Text style={styles.datetext}>{item.jobStartDate}</Text>
              </View>
              <View style={styles.datecontainer}>
                <Text style={styles.datetitle}>Last Date :- </Text>
                <Text style={styles.datetext}>{item.jobEndDate}</Text>
              </View>
              <View style={styles.downloadBtnContainer}>
                <Button
                  color={Colors.gray}
                  style={styles.downloadBtn}
                  labelStyle={styles.downloadBtnLabel}
                  onPress={() => handlDownload(item.jobPdfUrl)}>
                  <FontAwesome name="download" /> Notification
                </Button>
              </View>
              {/*This below ScrollView used for Removing the warning of VirtualizedLists should 
                never be nested inside plain ScrollViews with the same orientation because 
                it can break windowing and other functionality - 
                use another VirtualizedList-backed container instead */}
              <ScrollView
                contentContainerStyle={{width: '100%'}}
                horizontal
                scrollEnabled={false}>
                <FlatList
                  scrollEnabled={false}
                  data={posts}
                  renderItem={({item, index}) => (
                    <PostItem
                      item={item}
                      index={index}
                      handleSelectPost={handleSelectPost}
                      selectedPost={jobApply}
                    />
                  )}
                />
              </ScrollView>
            </View>
          </View>
          <View style={{justifyContent: 'center', alignItems: 'center'}}>
            <Button
              style={styles.applybtn}
              labelStyle={styles.btnlable}
              onPress={CheckOut}>
              Apply Now
            </Button>
          </View>
        </ScrollView>
      )}
    </>
  );
};

export default JobDetail;

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  detailscontainer: {
    backgroundColor: '#4CA6A8',
    height: 170,
    justifyContent: 'center',
    alignItems: 'center',
  },
  jobtitle: {
    color: '#fff',
    fontSize: 25,
    fontWeight: '600',
    letterSpacing: 0.75,
  },
  jobcategory: {
    marginTop: 10,
    color: '#fff',
    fontSize: 15,
    fontWeight: '500',
    letterSpacing: 1,
  },
  // centerbtn: {
  //   flexDirection: 'row',
  //   justifyContent: 'center',
  // },
  descriptioncontainer: {
    flex: 1,
    borderWidth: 1,
    borderColor: 'red',
  },
  heading: {
    fontSize: 16,
    fontWeight: '700',
    color: '#4CA6A8',
  },
  heading2: {
    color: '#4CA6A8',
    marginTop: 10,
    fontSize: 16,
    fontWeight: '700',
  },
  datecontainer: {
    flexDirection: 'row',
    paddingTop: 3,
  },
  datetitle: {
    fontSize: 14,
    fontWeight: '700',
    color: 'black',
  },
  countjob: {
    marginTop: -20,
    padding: 5,
    backgroundColor: '#fff',
    width: '45%',
  },
  btnlabl: {
    color: '#4CA6A8',
  },
  descriptioncontainer: {
    marginTop: 15,
    paddingHorizontal: 20,
  },
  jobDescription: {
    fontSize: 15,
  },

  applybtn: {
    backgroundColor: '#4CA6A8',
    width: '85%',
    marginTop: 20,
    padding: 5,
  },
  btnlable: {
    color: 'white',
  },
  downloadBtnContainer: {
    marginTop: 10,
    width: '50%',
    alignSelf: 'center',
  },
  downloadBtn: {},
  downloadBtnLabel: {
    color: Colors.primary,
  },
});
