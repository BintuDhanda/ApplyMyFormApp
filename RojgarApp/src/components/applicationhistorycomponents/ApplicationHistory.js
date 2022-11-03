import React, {useCallback, useContext, useEffect, useState} from 'react';
import {
  View,
  StyleSheet,
  FlatList,
  RefreshControl,
  Dimensions,
} from 'react-native';
import {CFetch} from '../../settings/APIFetch';
import {AppContext} from '../../settings/Context';
import Loader from '../../utilities/Loader';
import Message from '../../utilities/Message';
import ApplicationHistoryItem from './ApplicationHistoryItem';

const {height, width} = Dimensions.get('window');
const ApplicationHistory = () => {
  const {appUser} = useContext(AppContext);
  const [loading, setLoading] = useState(true);
  const [waiting, setWaiting] = useState(false);
  const [allLoaded, setAllLoaded] = useState(false);
  const [refreshing, setRefreshing] = useState(false);
  const take = 10;
  const [skip, setSkip] = useState(take);
  const [data, setData] = useState([]);
  const onRefresh = useCallback(() => {
    setRefreshing(true);
    setAllLoaded(false);
    loadData();
  }, []);
  const loadData = isSkip => {
    CFetch('ApplicationHistory', appUser.token, {
      skip: isSkip ? skip : 0,
      take: take,
    })
      .then(res => {
        if (res.status === 200) {
          res.json().then(result => {
            
            if (result.length > 0) {
              if (isSkip) {
                setData([...data, ...result]);
                setSkip(skip + take);
              } else {
                setData(result);
                setSkip(take);
              }
            } else {
              setAllLoaded(true);
            }
          });
        }
      })
      .catch(error => {
        console.log(error);
      })
      .finally(() => {
        setTimeout(() => {
          setLoading(false);
          setWaiting(false);
          setRefreshing(false);
        }, 10);
      });
  };

  useEffect(() => {
    loadData();
  }, []);

  return (
    <View style={styles.container}>
      <Loader loading={loading} waiting={waiting} spinner={true} />
      <FlatList
        contentContainerStyle={{paddingBottom: 10}}
        data={data}
        refreshControl={
          <RefreshControl refreshing={refreshing} onRefresh={onRefresh} />
        }
        ListEmptyComponent={() => (
          <Message
            height={height - 100}
            message="No History"
            allLoaded={true}
          />
        )}
        ListFooterComponent={() =>
          data.length > 0 && <Message allLoaded={allLoaded} />
        }
        renderItem={({item, index}) => (
          <ApplicationHistoryItem item={item} index={index} />
        )}
        onEndReached={() => loadData(true)}
        onEndReachedThreshold={0.5}
      />
    </View>
  );
};

export default ApplicationHistory;

const styles = StyleSheet.create({
  container: {
    justifyContent: 'center',
    alignItems: 'center',
    flex: 1,
  },
});
