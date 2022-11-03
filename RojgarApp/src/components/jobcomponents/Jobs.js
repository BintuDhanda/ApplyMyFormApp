import React, {useCallback, useContext, useEffect, useState} from 'react';
import {
  View,
  StyleSheet,
  FlatList,
  RefreshControl,
  Dimensions,
  TextInput,
} from 'react-native';
import {CFetch} from '../../settings/APIFetch';
import {AppContext} from '../../settings/Context';
import Loader from '../../utilities/Loader';
import JobItem from './JobItem';
import Message from '../../utilities/Message';
import CategoryItem from './CategoryItem';
import SelectButton from '../../utilities/SelectButton';
import Colors from '../../settings/Colors';
const {height, width} = Dimensions.get('window');
const Jobs = () => {
  const {appUser} = useContext(AppContext);
  const [loading, setLoading] = useState(true);
  const [waiting, setWaiting] = useState(false);
  const [allLoaded, setAllLoaded] = useState(false);
  const [refreshing, setRefreshing] = useState(false);
  const take = 10;
  const [skip, setSkip] = useState(take);
  const takeCategory = 10;
  const [skipCategory, setSkipCategory] = useState(takeCategory);
  const [data, setData] = useState([]);
  const [categories, setCategories] = useState([]);
  const [selectedCategory, setSelectedCategory] = useState('All');
  const onRefresh = useCallback(() => {
    setRefreshing(true);
    setAllLoaded(false);
    setSelectedCategory('All');
    loadData();
  }, []);
  const loadData = isSkip => {
    CFetch('HomeItems', appUser.token, {
      skip: isSkip ? skip : 0,
      take: take,
      id: selectedCategory != 'All' && selectedCategory ? selectedCategory : 0,
    })
      .then(res => {
        if (res.status === 200) {
          res.json().then(result => {
            setCategories(result.categories);
            if (isSkip) {
              if (result.jobs.length > 0) {
                setData([...data, ...result.jobs]);
                setSkip(skip + take);
              } else {
                setAllLoaded(true);
              }
            } else {
              setData(result.jobs);
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
  const loadMoreCategory = () => {
    CFetch('MoreCategories', appUser.token, {
      skip: skipCategory,
      take: takeCategory,
    })
      .then(res => {
        if (res.status === 200) {
          res.json().then(result => {
            if (result.length > 0) {
              setCategories([...categories, ...result]);
              setSkipCategory(skipCategory + takeCategory);
            }
          });
        }
      })
      .catch(error => {
        console.log(error);
      });
  };
  useEffect(() => {
    setLoading(true);
    setWaiting(true);
    loadData();
  }, [selectedCategory]);
  useEffect(() => {
    loadData();
  }, []);
  const handleCategoryFilter = val => {
    setSelectedCategory(val);
  };
  return (
    <View style={styles.container}>
      <Loader loading={loading} waiting={waiting} spinner={true} />
      <FlatList
        contentContainerStyle={{
          alignItems: 'flex-start',
          marginLeft: 5,
          marginTop: 10,
          paddingVertical: 5,
          height: 55,
        }}
        showsHorizontalScrollIndicator={false}
        ListHeaderComponent={() => (
          <SelectButton
            key={-1}
            label={'All'}
            style={styles.btn}
            labelStyle={styles.btnLabel}
            selectedColor={Colors.gray}
            value={'All'}
            selectedValue={selectedCategory}
            onSelectValue={handleCategoryFilter}
          />
        )}
        horizontal
        data={categories}
        renderItem={({item, index}) => (
          <CategoryItem
            item={item}
            index={index}
            handleCategoryFilter={handleCategoryFilter}
            selectedCategory={selectedCategory}
            btnStyle={styles.btn}
            btnLabelStyle={styles.btnLabel}
          />
        )}
        onEndReached={() => loadMoreCategory()}
        onEndReachedThreshold={0.5}
      />
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
        renderItem={({item, index}) => <JobItem item={item} index={index} />}
        onEndReached={() => loadData(true)}
        onEndReachedThreshold={0.5}
      />
    </View>
  );
};

export default Jobs;

const styles = StyleSheet.create({
  container: {
    // flex: 1,
  },
  input: {
    height: 45,
    margin: 12,
    borderWidth: 1,
    padding: 10,
    borderRadius: 25,
    width: '80%',
    borderColor: '#4CA6A8',
  },
  btn: {
    backgroundColor: '#4CA6A8',
    elevation: 5,
    borderRadius: 25,
  },
  btnLabel: {
    paddingHorizontal: 20,
    paddingVertical: 5,
    color: 'white',
  },
});
