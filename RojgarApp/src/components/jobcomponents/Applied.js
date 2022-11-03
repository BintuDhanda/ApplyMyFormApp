import React, {useCallback, useContext, useEffect, useState} from 'react';
import {FlatList, RefreshControl, Dimensions} from 'react-native';
import {CFetch} from '../../settings/APIFetch';
import {AppContext} from '../../settings/Context';
import Loader from '../../utilities/Loader';
import AppliedItem from './AppliedItem';
import Message from '../../utilities/Message';

const {height, width} = Dimensions.get('window');
const Applied = () => {
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
    CFetch('AppliedJobs', appUser.token, {
      skip: isSkip ? skip : 0,
      take: take,
    })
      .then(res => {
        if (res.status === 200) {
          res.json().then(result => {
            if (isSkip) {
              if (result.length > 0) {
                setData([...data, ...result]);
                setSkip(skip + take);
              } else {
                setAllLoaded(true);
              }
            } else {
              setData(result);
              setSkip(take);
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
    <>
      <Loader loading={loading} waiting={waiting} spinner={true} />
      <FlatList
        style={{height: '100%', padding: 10}}
        contentContainerStyle={{
          paddingBottom: 10,
        }}
        data={data}
        refreshControl={
          <RefreshControl refreshing={refreshing} onRefresh={onRefresh} />
        }
        ListEmptyComponent={() => (
          <Message height={height - 100} message="No Jobs" allLoaded={true} />
        )}
        ListFooterComponent={() =>
          data.length > 0 && <Message allLoaded={allLoaded} />
        }
        renderItem={({item, index}) => (
          <AppliedItem item={item} index={index} />
        )}
        onEndReached={() => loadData(true)}
        onEndReachedThreshold={0.5}
      />
    </>
  );
};

export default Applied;
